using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaoCaoWebNangCao.Models
{
    public class mapTaikhoan
    {
        WEBNANGCAOEntities db = new WEBNANGCAOEntities();
        public TAIKHOAN Timkiem(string TENDANGNHAP,string MATKHAU,string LOAITAIKHOAN )
        {
            var user = db.TAIKHOANs.Where(m => m.TENDANGNHAP == TENDANGNHAP & m.MATKHAU==MATKHAU & m.LOAITAIKHOAN==1).ToList();
            if(user.Count>0)
            {
                return user[0];
            }
            else
            {
                return null;
            }    
        }

        public TAIKHOAN Timkiem2(string TENDANGNHAP, string MATKHAU, string LOAITAIKHOAN)
        {
            var user2 = db.TAIKHOANs.Where(m => m.TENDANGNHAP == TENDANGNHAP & m.MATKHAU==MATKHAU & m.LOAITAIKHOAN==2).ToList();
            if (user2.Count>0)
            {
                return user2[0];
            }
            else
            {
                return null;
            }
        }
    }
}