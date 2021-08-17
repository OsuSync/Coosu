﻿using System.Numerics;
using Coosu.Storyboard.Easing;
using Coosu.Storyboard.Events;

namespace Coosu.Storyboard
{
    partial class Sprite
    {
        // Move
        public void Move(int startTime, Vector2 point) =>
            AddEvent(EventTypes.Move, LinearEase.Instance, startTime, startTime, point.X, point.Y, point.X, point.Y);
        public void Move(int startTime, double x, double y) =>
            AddEvent(EventTypes.Move, LinearEase.Instance, startTime, startTime, x, y, x, y);
        public void Move(int startTime, int endTime, double x, double y) =>
            AddEvent(EventTypes.Move, LinearEase.Instance, startTime, endTime, x, y, x, y);
        public void Move(int startTime, int endTime, double x1, double y1, double x2, double y2) =>
            AddEvent(EventTypes.Move, LinearEase.Instance, startTime, endTime, x1, y1, x2, y2);
        public void Move(int startTime, int endTime, Vector2 startPoint, Vector2 endPoint) =>
            AddEvent(EventTypes.Move, LinearEase.Instance, startTime, endTime, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        public void Move(EasingFunctionBase easing, int startTime, int endTime, Vector2 startPoint, Vector2 endPoint) =>
               AddEvent(EventTypes.Move, easing, startTime, endTime, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        public void Move(EasingFunctionBase easing, int startTime, int endTime, double x1, double y1, double x2, double y2) =>
            AddEvent(EventTypes.Move, easing, startTime, endTime, x1, y1, x2, y2);

        // Fade
        public void Fade(int startTime, double opacity) =>
            AddEvent(EventTypes.Fade, LinearEase.Instance, startTime, startTime, opacity, opacity);
        public void Fade(int startTime, int endTime, double opacity) =>
            AddEvent(EventTypes.Fade, LinearEase.Instance, startTime, endTime, opacity, opacity);
        public void Fade(int startTime, int endTime, double startOpacity, double endOpacity) =>
            AddEvent(EventTypes.Fade, LinearEase.Instance, startTime, endTime, startOpacity, endOpacity);
        public void Fade(EasingFunctionBase easing, int startTime, int endTime, double startOpacity, double endOpacity) =>
               AddEvent(EventTypes.Fade, easing, startTime, endTime, startOpacity, endOpacity);

        // Scale
        public void Scale(int startTime, double scale) =>
            AddEvent(EventTypes.Scale, LinearEase.Instance, startTime, startTime, scale, scale);
        public void Scale(int startTime, int endTime, double scale) =>
            AddEvent(EventTypes.Scale, LinearEase.Instance, startTime, endTime, scale, scale);
        public void Scale(int startTime, int endTime, double startScale, double endScale) =>
            AddEvent(EventTypes.Scale, LinearEase.Instance, startTime, endTime, startScale, endScale);
        public void Scale(EasingFunctionBase easing, int startTime, int endTime, double startScale, double endScale) =>
                  AddEvent(EventTypes.Scale, easing, startTime, endTime, startScale, endScale);

        // Rotate
        public void Rotate(int startTime, double rotate) =>
            AddEvent(EventTypes.Rotate, LinearEase.Instance, startTime, startTime, rotate, rotate);
        public void Rotate(int startTime, int endTime, double rotate) =>
            AddEvent(EventTypes.Rotate, LinearEase.Instance, startTime, endTime, rotate, rotate);
        public void Rotate(int startTime, int endTime, double startRotate, double endRotate) =>
            AddEvent(EventTypes.Rotate, LinearEase.Instance, startTime, endTime, startRotate, endRotate);
        public void Rotate(EasingFunctionBase easing, int startTime, int endTime, double startRotate, double endRotate) =>
            AddEvent(EventTypes.Rotate, easing, startTime, endTime, startRotate, endRotate);

        // MoveX
        public void MoveX(int startTime, double x) =>
            AddEvent(EventTypes.MoveX, LinearEase.Instance, startTime, startTime, x, x);
        public void MoveX(int startTime, int endTime, double x) =>
            AddEvent(EventTypes.MoveX, LinearEase.Instance, startTime, endTime, x, x);
        public void MoveX(int startTime, int endTime, double startX, double endX) =>
            AddEvent(EventTypes.MoveX, LinearEase.Instance, startTime, endTime, startX, endX);
        public void MoveX(EasingFunctionBase easing, int startTime, int endTime, double startX, double endX) =>
            AddEvent(EventTypes.MoveX, easing, startTime, endTime, startX, endX);

        // MoveY
        public void MoveY(int startTime, double y) =>
            AddEvent(EventTypes.MoveY, LinearEase.Instance, startTime, startTime, y, y);
        public void MoveY(int startTime, int endTime, double y) =>
            AddEvent(EventTypes.MoveY, LinearEase.Instance, startTime, endTime, y, y);
        public void MoveY(int startTime, int endTime, double startY, double endY) =>
            AddEvent(EventTypes.MoveY, LinearEase.Instance, startTime, endTime, startY, endY);
        public void MoveY(EasingFunctionBase easing, int startTime, int endTime, double startY, double endY) =>
            AddEvent(EventTypes.MoveY, easing, startTime, endTime, startY, endY);

        // Color
        public void Color(int startTime, Vector3 color) =>
            AddEvent(EventTypes.Color, LinearEase.Instance, startTime, startTime, color.X, color.Y, color.Z, color.X, color.Y, color.Z);
        public void Color(int startTime, int endTime, Vector3 color) =>
            AddEvent(EventTypes.Color, LinearEase.Instance, startTime, endTime, color.X, color.Y, color.Z, color.X, color.Y, color.Z);
        public void Color(int startTime, int endTime, Vector3 color1, Vector3 color2) =>
            AddEvent(EventTypes.Color, LinearEase.Instance, startTime, endTime, color1.X, color1.Y, color1.Z, color2.X, color2.Y, color2.Z);
        public void Color(EasingFunctionBase easing, int startTime, int endTime, Vector3 color1, Vector3 color2) =>
            AddEvent(EventTypes.Color, easing, startTime, endTime, color1.X, color1.Y, color1.Z, color2.X, color2.Y, color2.Z);
        public void Color(int startTime, int r, int g, int b) =>
            AddEvent(EventTypes.Color, LinearEase.Instance, startTime, startTime, r, g, b, r, g, b);
        public void Color(int startTime, int endTime, int r, int g, int b) =>
            AddEvent(EventTypes.Color, LinearEase.Instance, startTime, endTime, r, g, b, r, g, b);
        public void Color(int startTime, int endTime, int startR, int startG, int startB, int endR, int endG, int endB) =>
            AddEvent(EventTypes.Color, LinearEase.Instance, startTime, endTime, startR, startG, startB, endR, endG, endB);
        public void Color(EasingFunctionBase easing, int startTime, int endTime, int startR, int startG, int startB, int endR, int endG, int endB) =>
            AddEvent(EventTypes.Color, easing, startTime, endTime, startR, startG, startB, endR, endG, endB);
        public void Color(EasingFunctionBase easing, int startTime, int endTime, double startR, double startG, double startB, double endR, double endG, double endB) =>
            AddEvent(EventTypes.Color, easing, startTime, endTime, startR, startG, startB, endR, endG, endB);

        // Vector
        public void Vector(int startTime, Vector2 vector) =>
            AddEvent(EventTypes.Vector, LinearEase.Instance, startTime, startTime, vector.X, vector.Y, vector.X, vector.Y);
        public void Vector(int startTime, double w, double h) =>
            AddEvent(EventTypes.Vector, LinearEase.Instance, startTime, startTime, w, h, w, h);
        public void Vector(int startTime, int endTime, double w, double h) =>
            AddEvent(EventTypes.Vector, LinearEase.Instance, startTime, endTime, w, h, w, h);
        public void Vector(int startTime, int endTime, Vector2 startZoom, Vector2 endZoom) =>
            AddEvent(EventTypes.Vector, LinearEase.Instance, startTime, endTime, startZoom.X, startZoom.Y, endZoom.X, endZoom.Y);
        public void Vector(EasingFunctionBase easing, int startTime, int endTime, Vector2 startZoom, Vector2 endZoom) =>
            AddEvent(EventTypes.Vector, easing, startTime, endTime, startZoom.X, startZoom.Y, endZoom.X, endZoom.Y);
        public void Vector(int startTime, int endTime, double w1, double h1, double w2, double h2) =>
            AddEvent(EventTypes.Vector, LinearEase.Instance, startTime, endTime, w1, h1, w2, h2);
        public void Vector(EasingFunctionBase easing, int startTime, int endTime, double w1, double h1, double w2, double h2) =>
            AddEvent(EventTypes.Vector, easing, startTime, endTime, w1, h1, w2, h2);

        //Extra
        public void FlipH(int startTime) => AddEvent(startTime, startTime, ParameterType.Horizontal);
        public void FlipH(int startTime, int endTime) => AddEvent(startTime, endTime, ParameterType.Horizontal);

        public void FlipV(int startTime) => AddEvent(startTime, startTime, ParameterType.Vertical);
        public void FlipV(int startTime, int endTime) => AddEvent(startTime, endTime, ParameterType.Vertical);

        public void Additive(int startTime) =>
            AddEvent(startTime, startTime, ParameterType.Additive);
        public void Additive(int startTime, int endTime) =>
            AddEvent(startTime, endTime, ParameterType.Additive);

        public void Parameter(int startTime, int endTime, ParameterType p) =>
            AddEvent(startTime, endTime, p);


        public void AddEvent(double startTime, double endTime, ParameterType p)
        {
            AddEvent(CommonEvent.Create(EventTypes.Parameter, EasingType.Linear, startTime, endTime,
                new[] { (double)(int)p }, new[] { (double)(int)p }));
        }

        public void AddEvent(EventType e, EasingFunctionBase easing, double startTime, double endTime,
            double x1, double x2)
        {
            AddEvent(CommonEvent.Create(e, easing, startTime, endTime, new[] { x1 }, new[] { x2 }));
        }

        public void AddEvent(EventType e, EasingFunctionBase easing, double startTime, double endTime,
            double x1, double y1, double x2, double y2)
        {
            AddEvent(CommonEvent.Create(e, easing, startTime, endTime, new[] { x1, y1 }, new[] { x2, y2 }));
        }

        public void AddEvent(EventType e, EasingFunctionBase easing, double startTime, double endTime,
            double x1, double y1, double z1, double x2, double y2, double z2)
        {
            AddEvent(CommonEvent.Create(e, easing, startTime, endTime, new[] { x1, y1, z1 }, new[] { x2, y2, z2 }));
        }
    }
}
