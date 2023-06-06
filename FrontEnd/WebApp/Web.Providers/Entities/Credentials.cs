using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Providers.Entities
{
	public class Credentials
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}

