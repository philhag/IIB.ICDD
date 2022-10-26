namespace IIB.ICDD;

/// <summary>
/// Icdd Toolkit Library
/// Class:  IcddContainerReaderOptions 
/// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
/// Mail: philipp.hagedorn-n6v@rub.de 
/// </summary>
public class IcddContainerReaderOptions
{
    internal string CustomWorkfolder;
    internal bool UseCustomWorkfolder;

    internal string CustomGuid;
    internal bool UseCustomGuid;

    /// <summary>
    /// Specifies container reader options wih a workfolder path
    /// </summary>
    /// <param name="workfolder"></param>
    public IcddContainerReaderOptions(string workfolder)
    {
        UseCustomWorkfolder = true;
        CustomWorkfolder = workfolder;
    }
    /// <summary>
    /// Specifies container reader options wih a workfolder path and a predefined container guid that shall be applied to the container
    /// </summary>
    /// <param name="workfolder"></param>
    /// <param name="guid"></param>
    public IcddContainerReaderOptions(string workfolder, string guid)
    {
        UseCustomWorkfolder = true;
        CustomWorkfolder = workfolder;

        UseCustomGuid = true;
        CustomGuid = guid;
    }

}