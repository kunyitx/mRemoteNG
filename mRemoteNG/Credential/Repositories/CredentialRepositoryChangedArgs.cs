using System;

namespace mRemoteNG.Credential.Repositories;

public class CredentialRepositoryChangedArgs : EventArgs
{
    public CredentialRepositoryChangedArgs(ICredentialRepository repository)
    {
        if (repository == null)
            throw new ArgumentNullException(nameof(repository));

        Repository = repository;
    }

    public ICredentialRepository Repository { get; }
}