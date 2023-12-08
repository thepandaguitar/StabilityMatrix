﻿using System.Collections.Immutable;

namespace StabilityMatrix.Core.Models.Api.Comfy;

public readonly record struct ComfyScheduler(string Name)
{
    public static ComfyScheduler Normal { get; } = new("normal");
    public static ComfyScheduler Karras { get; } = new("karras");
    public static ComfyScheduler Exponential { get; } = new("exponential");
    public static ComfyScheduler SDTurbo { get; } = new("sd_turbo");

    private static Dictionary<string, string> ConvertDict { get; } =
        new()
        {
            [Normal.Name] = "Normal",
            [Karras.Name] = "Karras",
            [Exponential.Name] = "Exponential",
            ["sgm_uniform"] = "SGM Uniform",
            ["simple"] = "Simple",
            ["ddim_uniform"] = "DDIM Uniform",
            [SDTurbo.Name] = "SD Turbo"
        };

    public static IReadOnlyList<ComfyScheduler> Defaults { get; } =
        ConvertDict.Keys.Select(k => new ComfyScheduler(k)).ToImmutableArray();

    public string DisplayName => ConvertDict.GetValueOrDefault(Name, Name);

    private sealed class NameEqualityComparer : IEqualityComparer<ComfyScheduler>
    {
        public bool Equals(ComfyScheduler x, ComfyScheduler y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(ComfyScheduler obj)
        {
            return obj.Name.GetHashCode();
        }
    }

    public static IEqualityComparer<ComfyScheduler> Comparer { get; } = new NameEqualityComparer();
}
