# scrolling-listview-without-virtualization-xamarin
Scrolling listview without virtualization with autofit

## Sample

```xaml
<ScrollView>
    <syncfusion:SfListView x:Name="listView" AutoFitMode="Height" ItemsSource="{Binding ContactsInfo}">
        <syncfusion:SfListView.ItemTemplate >
            <DataTemplate>
                <code>
                . . .
                . . .
                <code>
            </DataTemplate>
        </syncfusion:SfListView.ItemTemplate>
    </syncfusion:SfListView>
</ScrollView>

C#:

visualContainer = listView.GetVisualContainer();
visualContainer.PropertyChanged += Container_PropertyChanged;
listView.Loaded += ListView_Loaded;

private void Container_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
{
    if (e.PropertyName == "Height" && listView.HeightRequest != visualContainer.Height && loaded)
        listView.HeightRequest = visualContainer.Height;
}

private void ListView_Loaded(object sender, ListViewLoadedEventArgs e)
{
    var extent = (double)visualContainer.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "TotalExtent").GetValue(visualContainer);
    listView.HeightRequest = extent;
    loaded = true;
}
```