using System;
using System.ComponentModel;
using mRemoteNG.App;
using mRemoteNG.Messages;

namespace mRemoteNG.Connection;

public class DefaultConnectionInheritance : ConnectionInfoInheritance
{
    static DefaultConnectionInheritance()
    {
    }

    private DefaultConnectionInheritance() : base(null, true)
    {
    }

    [Browsable(false)] public static DefaultConnectionInheritance Instance { get; } = new();


    public void LoadFrom<TSource>(TSource sourceInstance, Func<string, string> propertyNameMutator = null)
    {
        if (propertyNameMutator == null) propertyNameMutator = a => a;
        var inheritanceProperties = GetProperties();
        foreach (var property in inheritanceProperties)
        {
            var propertyFromSettings = typeof(TSource).GetProperty(propertyNameMutator(property.Name));
            if (propertyFromSettings == null)
            {
                Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg,
                    $"DefaultConInherit-LoadFrom: Could not load {property.Name}",
                    true);
                continue;
            }

            var valueFromSettings = propertyFromSettings.GetValue(sourceInstance, null);
            property.SetValue(Instance, valueFromSettings, null);
        }
    }

    public void SaveTo<TDestination>(TDestination destinationInstance,
        Func<string, string> propertyNameMutator = null)
    {
        if (propertyNameMutator == null) propertyNameMutator = a => a;
        var inheritanceProperties = GetProperties();
        foreach (var property in inheritanceProperties)
        {
            var propertyFromSettings = typeof(TDestination).GetProperty(propertyNameMutator(property.Name));
            var localValue = property.GetValue(Instance, null);
            if (propertyFromSettings == null)
            {
                Runtime.MessageCollector?.AddMessage(MessageClass.ErrorMsg,
                    $"DefaultConInherit-SaveTo: Could not load {property.Name}",
                    true);
                continue;
            }

            propertyFromSettings.SetValue(destinationInstance, localValue, null);
        }
    }
}