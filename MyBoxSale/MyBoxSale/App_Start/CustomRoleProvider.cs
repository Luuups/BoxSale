using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MyBoxSale.Models;

namespace MyBoxSale
{
    public class CustomRoleProvider:RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            using (MyBoxSaleEntities db = new MyBoxSaleEntities())
            {
                if (!Roles.Provider.RoleExists("Admin"))
                {
                    Roles.CreateRole("Admin");
                }
                if (!Roles.Provider.RoleExists("Cajero"))
                {
                    Roles.CreateRole("Cajero");
                }
                foreach(var user in usernames)
                    foreach (var rol in roleNames)
                    {
                        db.USUARIOROLES.Add(new USUARIOROLES { UserId = db.USUARIO.SingleOrDefault(x => x.NombreUsuario.Equals(user, StringComparison.CurrentCultureIgnoreCase)).Id, RolId = db.ROLES.SingleOrDefault(x => x.Nombre.Equals(rol, StringComparison.CurrentCultureIgnoreCase)).Id,Activo=true });
                        db.SaveChanges();
                    }
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            using (MyBoxSaleEntities db = new MyBoxSaleEntities())
            {
                db.ROLES.Add(new ROLES { Nombre = roleName, Activo=true });
                db.SaveChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (MyBoxSaleEntities db = new MyBoxSaleEntities())
            {
                USUARIO user = db.USUARIO.FirstOrDefault(u => u.NombreUsuario.Equals(username, StringComparison.CurrentCultureIgnoreCase));
                if (user != null)
                {
                    var Roles = from ur in user.USUARIOROLES
                                from r in db.ROLES
                                where ur.RolId == r.Id
                                select r.Nombre;
                    if (Roles != null)
                        return Roles.ToArray();
                    else
                        return new string[] { };
                }
                return new string[] { };
                
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (MyBoxSaleEntities db = new MyBoxSaleEntities())
            {
                USUARIO user = db.USUARIO.FirstOrDefault(u => u.NombreUsuario.Equals(username, StringComparison.CurrentCultureIgnoreCase));
                var roles = from ur in user.USUARIOROLES
                            from r in db.ROLES
                            where ur.RolId == r.Id
                            select r.Nombre;
                if (user != null)
                    return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
                else
                    return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            using (MyBoxSaleEntities db = new MyBoxSaleEntities())
            {
                return db.ROLES.ToList().Any(x=>x.Nombre.Equals(roleName,StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}