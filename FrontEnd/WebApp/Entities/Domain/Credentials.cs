using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
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

