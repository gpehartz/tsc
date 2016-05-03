﻿namespace Tsc.Application
{
    public interface ITranslator
    {
        Domain.Team TranslateToDomain(ServiceModel.Team team);

        Domain.Tournament TranslateToDomain(ServiceModel.Tournament tournament);

        ServiceModel.Tournament TranslateToService(Domain.Tournament tournament);

        ServiceModel.Team TranslateToService(Domain.Team team);
    }
}