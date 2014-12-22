namespace JCotton.DismSharp.Interop {
    public enum DismCommitAndUnmountFlags : uint {
        Commit = 0x00000000,
        Discard = 0x00000001,
        GenerateIntegrity = 0x00010000,
        Append = 0x00020000,
        CommitMask = 0xffff0000
    }
}