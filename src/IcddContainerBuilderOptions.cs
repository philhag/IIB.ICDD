using IIB.ICDD.Parsing.Vocabulary;

namespace IIB.ICDD;

/// <summary>
/// Icdd Toolkit Library
/// Class:  IcddContainerBuilderOptions 
/// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
/// Mail: philipp.hagedorn-n6v@rub.de 
/// </summary>
public class IcddContainerBuilderOptions
{
    /// <summary>
    /// Gets or sets a custom workfolder
    /// </summary>
    public string CustomWorkfolder { get; set; }
    /// <summary>
    /// Gets or sets a custom guid
    /// </summary>
    public string CustomGuid { get; set; }
    /// <summary>
    /// Gets or sets the container file name
    /// </summary>
    public string ContainerName { get; set; }

    /// <summary>
    /// Gets the container namespace
    /// </summary>
    public string Namespace => string.IsNullOrEmpty(ContainerName) ? "" : IcddNamespacesHelper.BaseNamespaceFor(ContainerName, "index", "");
    /// <summary>
    /// Returns whether custom workfolder shall be used or not
    /// </summary>
    public bool UseCustomWorkfolder => string.IsNullOrEmpty(CustomWorkfolder);
    /// <summary>
    /// Returns whether custom guid shall be used or not
    /// </summary>
    public bool UseCustomGuid => string.IsNullOrEmpty(CustomWorkfolder);
}