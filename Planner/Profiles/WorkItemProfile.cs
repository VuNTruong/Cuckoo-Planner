using System;
using AutoMapper;
using Planner.Models;
using Planner.ViewModels;

namespace Planner.Profiles
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<WorkItem, WorkItemViewModel>();

            CreateMap<WorkItemViewModel, WorkItem>();
        }
    }
}
