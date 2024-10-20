using Application.InterfacesService;
using Domain.Entities;
using Domain.InterfacesRepositories;
using AutoMapper;
using Application.DTOs;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Usuario> Register(UsuarioDto registerDto)
        {
            var usuario = _mapper.Map<Usuario>(registerDto);
            return await _userRepository.Register(usuario, registerDto.Senha);
        }
    }
}
