namespace JCotton.DismSharp {
    public enum PackageFeatureState {
        DismStateNotPresent = 0,
        DismStateUninstallPending = 1,
        DismStateStaged = 2,
        DismStateResolved = 3,
        DismStateRemoved = 3,
        DismStateInstalled = 4,
        DismStateInstallPending = 5,
        DismStateSuperseded = 6,
        DismStatePartiallyInstalled = 7
    }
}
