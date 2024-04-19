using AutoMapper;
using Project.ProjectDataBase.Domain;

namespace Project.DTOs.DomainDTOs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, AppUserD>();
            CreateMap<City, CityD>();
            CreateMap<Collegian, CollegianD>();
            CreateMap<CollegianGroup, CollegianGroupD>();
            CreateMap<Field, FieldD>();
            CreateMap<Group, GroupD>();
            CreateMap<University, UniversityD>();
            CreateMap<InternshipLocation, InternshipLocationD>();
            CreateMap<Master, MasterD>();
            CreateMap<MasterEvaluation, MasterEvaluationD>();
            CreateMap<MasterEvaluationScore, MasterEvaluationScoreD>();
            CreateMap<Province, ProvinceD>();
            CreateMap<SupervisorEvaluation, SupervisorEvaluationD>();
            CreateMap<SupervisorEvaluationScore, SupervisorEvaluationScoreD>();
            CreateMap<Term, TermD>();
            CreateMap<UserImage, UserImageD>();
        }
    }
}
