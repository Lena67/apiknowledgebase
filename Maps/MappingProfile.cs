namespace knowledgebaseapi.Maps;
using AutoMapper;
using knowledgebaseapi.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Notebook, NotebookDto>();
        CreateMap<NotebookDto, Notebook>();
    }
    
}
