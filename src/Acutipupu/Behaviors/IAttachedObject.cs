using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Behaviors;

internal interface IAttachedObject
{
    void AttachTo(ObservableObject? bindable);

    void DetachFrom(ObservableObject? bindable);
}
