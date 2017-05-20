using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WebApplication2.Models
{   
	public  class 數量ViewRepository : EFRepository<數量View>, I數量ViewRepository
	{

	}

	public  interface I數量ViewRepository : IRepository<數量View>
	{

	}
}