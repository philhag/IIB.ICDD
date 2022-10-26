namespace IIB.ICDD.Model.Interfaces
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Interface:  IIcddVersioning 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public interface IIcddVersioning
    {
        string VersionId { get; set; }
        string VersionDescription { get; set; }

        IcddBaseElement NextVersion(string versionID, string versionDescription, InformationContainer copyToContainer = null);

    }
}
