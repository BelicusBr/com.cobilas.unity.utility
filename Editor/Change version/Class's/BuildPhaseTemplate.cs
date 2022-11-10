namespace Cobilas.Unity.Editor.Utility.ChangeVersion {
    public sealed class BuildPhaseTemplate : BaseBuildPhaseTemplate {
        public override string[] GetTemplates()
            => new string[] {
                "alpha",
                "beta"
            };
    }
}