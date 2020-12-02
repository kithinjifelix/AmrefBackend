﻿using System;
using PG.Management.Core.Domain.Consultation.Models;
using PG.SharedKernel.Interfaces.Persistence;

namespace PG.Management.Core.Domain.Consultation
{
    public interface IProjectObjectiveRepository : IRepository<ProjectObjective, Guid>
    {
        
    }
}