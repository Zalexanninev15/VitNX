namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Docking
{
    internal class DockDropCollection
    {
        internal DockDropArea DropArea { get; private set; }
        internal DockDropArea InsertBeforeArea { get; private set; }
        internal DockDropArea InsertAfterArea { get; private set; }

        internal DockDropCollection(VitNX_DockPanel dockPanel,
            VitNX_DockGroup group)
        {
            DropArea = new DockDropArea(dockPanel, group,
                DockInsertType.None);
            InsertBeforeArea = new DockDropArea(dockPanel,
                group,
                DockInsertType.Before);
            InsertAfterArea = new DockDropArea(dockPanel,
                group,
                DockInsertType.After);
        }
    }
}