using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Coosu.Api.HttpClient;
using Coosu.Api.V2;
using Coosu.Api.V2.RequestModels;
using Coosu.Api.V2.ResponseModels;
using Coosu.Beatmap;
using Coosu.Beatmap.Extensions.Playback;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoosuUnitTest.Beatmap
{
    [TestClass]
    public class BeatmapTest
    {

        [TestMethod]
        public async Task ReadAndWrite()
        {
            //var folder = @"C:\Users\milkitic\Desktop\1002455 supercell - Giniro Hikousen  (Ttm bootleg Edit)";
            //var file = @"supercell - Giniro Hikousen  (Ttm bootleg Edit) (yf_bmp) [4K Hard].osu";
            var folder = @"C:\Users\milkitic\Downloads\1376486 Risshuu feat. Choko - Take [no video] (2)";
            var file = @"Risshuu feat. Choko - Take (yf_bmp) [Ta~ke take take take take take tatata~].osu";
            var osuFile = await OsuFile.ReadFromFileAsync(Path.Combine(folder, file));

            osuFile.SaveToDirectory(folder, osuFile.Metadata.Version + " (TEST)");
        }

        [TestMethod]
        public async Task ReadHitsounds()
        {
            var folder = @"C:\Users\milkitic\Downloads\18826 Shin Hae Chul - Sticks and Stones";
            var file = "Shin Hae Chul - Sticks and Stones (Bikko) [Madness].osu";
            var osuDir = new OsuDirectory(folder);
            await osuDir.InitializeAsync(file);

            var hitsoundList = await osuDir.GetHitsoundNodesAsync(osuDir.OsuFiles[0]);
            var playableNodes = hitsoundList
                .Where(k => k is PlayableNode { /*PlayablePriority: PlayablePriority.Primary*/ })
                .Cast<PlayableNode>()
                .ToList();
        }
    }
}
