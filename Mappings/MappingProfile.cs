using AutoMapper;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Driver, GetDriversDTO>();
    }
}