using System;

namespace Demo.Pl.VeiwModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString(); 
        }


    }
}
