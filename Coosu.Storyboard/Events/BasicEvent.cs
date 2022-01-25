﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Coosu.Shared;
using Coosu.Shared.Mathematics;
using Coosu.Storyboard.Common;
using Coosu.Storyboard.Easing;
using Coosu.Storyboard.Extensibility;
using Coosu.Storyboard.Utils;

namespace Coosu.Storyboard.Events
{
    [DebuggerDisplay("Expression = {DebuggerDisplay}")]
    public abstract class BasicEvent : IKeyEvent
    {
        private List<float> _values;
        private string DebuggerDisplay => this.GetHeaderString();

        public abstract EventType EventType { get; }
        public EasingFunctionBase Easing { get; set; } = LinearEase.Instance;
        public float StartTime { get; set; }
        public float EndTime { get; set; }

        public virtual float DefaultValue => 0;
        public IReadOnlyList<float> Values
        {
            get => _values;
            internal set => _values = (List<float>)value;
        }

#if NET5_0_OR_GREATER
        public virtual Span<float> GetStartsSpan()
        {
            Fill();
            var size = EventType.Size;
            var span = System.Runtime.InteropServices.CollectionsMarshal
                .AsSpan(_values)
                .Slice(0, size);
            return span;
        }

        public Span<float> GetEndsSpan()
        {
            Fill();
            var size = EventType.Size;
            var span = System.Runtime.InteropServices.CollectionsMarshal
                .AsSpan(_values)
                .Slice(size, size);
            return span;
        }
#endif

        public virtual bool IsHalfFilled => Values.Count == EventType.Size;
        public virtual bool IsFilled => Values.Count == EventType.Size * 2;

        public virtual float GetValue(int index)
        {
            if (index >= EventType.Size * 2)
                throw new ArgumentOutOfRangeException(nameof(index), index,
                    $"Incorrect parameter index for {EventType.Flag}");
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index,
                    $"Incorrect parameter index for {EventType.Flag}");

            return GetValueImpl(index);
        }

        public virtual void SetValue(int index, float value)
        {
            if (index >= EventType.Size * 2)
                throw new ArgumentOutOfRangeException(nameof(index), index,
                    $"Incorrect parameter index for {EventType.Flag}");
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index,
                    $"Incorrect parameter index for {EventType.Flag}");

            SetValueImpl(index, value);
        }

        public virtual void Fill()
        {
            Fill(EventType.Size * 2);
        }

        public virtual bool IsStartsEqualsEnds()
        {
            if (IsHalfFilled) return true;
            if (!IsFilled) return false;

            var size = EventType.Size;
            for (var i = 0; i < Values.Count / 2; i++)
            {
                var d0 = Values[i];
                var d1 = Values[i + size];
                if (!Precision.AlmostEquals(d0, d1))
                    return false;
            }

            return true;
        }

        public void AdjustTiming(float offset)
        {
            StartTime += offset;
            EndTime += offset;
        }

        public async Task WriteHeaderAsync(TextWriter writer)
        {
            await writer.WriteAsync(EventType.Flag);
            await writer.WriteAsync(',');
            var typeStr = ((int?)Easing.TryGetEasingType())?.ToString() ?? "?";
            await writer.WriteAsync(typeStr);
            await writer.WriteAsync(',');
            await writer.WriteAsync(Math.Round(StartTime));
            await writer.WriteAsync(',');
            if (!EndTime.Equals(StartTime))
                await writer.WriteAsync(Math.Round(EndTime));
            await writer.WriteAsync(',');
            await WriteExtraScriptAsync(writer);
        }

        public virtual async Task WriteScriptAsync(TextWriter writer) =>
            await WriteHeaderAsync(writer);

        protected BasicEvent()
        {
            _values = new List<float>();
        }

        protected BasicEvent(EasingFunctionBase easing, float startTime, float endTime, List<float> values)
        {
            Easing = easing;
            StartTime = startTime;
            EndTime = endTime;
            _values = values;
        }

        protected virtual async Task WriteExtraScriptAsync(TextWriter textWriter)
        {
            var sequenceEqual = IsStartsEqualsEnds();
            if (sequenceEqual)
                await WriteStartAsync(textWriter);
            else
                await WriteFullAsync(textWriter);
        }

        protected void Fill(int count)
        {
            if (count <= _values.Count) return;
            var index = count - 1;
            var size = EventType.Size;
            if (IsHalfFilled && !IsFilled)
            {
                _values.Capacity = size * 2;
                for (int i = 0; i < size; i++)
                {
                    _values.Add(_values[i]);
                }
            }
            else if (index < size)
            {
                _values.Capacity = size;
                while (_values.Count < size)
                {
                    _values.Add(DefaultValue);
                }
            }
            else
            {
                while (index > _values.Count - 1)
                {
                    _values.Add(DefaultValue);
                }
            }
        }

        protected float GetValueImpl(int index)
        {
            Fill(index + 1);
            return _values[index];
        }

        protected void SetValueImpl(int index, float value)
        {
            Fill(index + 1);
            _values[index] = value;
        }

        private async Task WriteStartAsync(TextWriter textWriter)
        {
            for (int i = 0; i < EventType.Size; i++)
            {
                await textWriter.WriteAsync(Values[i].ToIcString());
                if (i != EventType.Size - 1) await textWriter.WriteAsync(',');
            }
        }

        private async Task WriteFullAsync(TextWriter textWriter)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                await textWriter.WriteAsync(Values[i].ToIcString());
                if (i != Values.Count - 1) await textWriter.WriteAsync(',');
            }
        }

        //public static IKeyEvent Create(EventType e, EasingFunctionBase easing,
        //    double startTime, double endTime,
        //    List<double> value)
        //{
        //    var size = e.Size;
        //    if (size != 0 && value.Count != size && value.Count != size * 2)
        //        throw new ArgumentException();
        //    if (size == 0)
        //        return Create(e, easing, startTime, endTime, value.Slice(0, 1), default);
        //    return Create(e, easing, startTime, endTime,
        //        value.Slice(0, size),
        //        value.Length == size ? default : value.Slice(size, size));
        //}

        public static IKeyEvent Create(EventType e, EasingFunctionBase easing,
            float startTime, float endTime, List<float> values)
        {
            var size = e.Size;
            if (size != 0 && values.Count != size && values.Count != size * 2)
                throw new ArgumentException($"Incorrect parameter length for {e.Flag}: {values.Count}");

            IKeyEvent keyEvent;
            if (e == EventTypes.Fade)
                keyEvent = new Fade(easing, startTime, endTime, values);
            else if (e == EventTypes.Move)
                keyEvent = new Move(easing, startTime, endTime, values);
            else if (e == EventTypes.MoveX)
                keyEvent = new MoveX(easing, startTime, endTime, values);
            else if (e == EventTypes.MoveY)
                keyEvent = new MoveY(easing, startTime, endTime, values);
            else if (e == EventTypes.Scale)
                keyEvent = new Scale(easing, startTime, endTime, values);
            else if (e == EventTypes.Vector)
                keyEvent = new Vector(easing, startTime, endTime, values);
            else if (e == EventTypes.Rotate)
                keyEvent = new Rotate(easing, startTime, endTime, values);
            else if (e == EventTypes.Color)
                keyEvent = new Color(easing, startTime, endTime, values);
            else if (e == EventTypes.Parameter)
                keyEvent = new Parameter(startTime, endTime, values);
            else
            {
                var result = HandlerRegister.GetEventTransformation(e)?.Invoke(e, easing, startTime, endTime, values);
                keyEvent = result ?? throw new ArgumentOutOfRangeException(nameof(e), e, null);
            }

            //keyEvent.Fill();
            return keyEvent;
        }

        public object Clone()
        {
            return BasicEvent.Create(EventType, Easing, StartTime, EndTime, Values.ToList());
        }
    }
}
