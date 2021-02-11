using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarisServerTest
{
	public sealed class ClarisAnswer
	{
		/*
		 * Номер заявки - number
	•    Государственный регистрационный номер автомобиля - carNumber
	•    Марка автомобиля - carModelText
	•    Машиноместо - parkingPlace
	•    ФИО водителя/гостя - visitorFullName
	•    Дата парковки. В случае подачи долгосрочной заявки Дата окончания парковки.  visitDate / expirationDate
		 */

		public string carNumber { get; set; }

		public string carModelText { get; set; }

		public string parkingPlace { get; set; }

		public string visitorFullName { get; set; }

		public DateTime visitDate { get; set; }

		public DateTime expirationDate { get; set; }


		public ClarisAnswer(
			string Number, 
			string ModelText, 
			string ParkingPlace, 
			string VisitorFullName,
			DateTime VisitDate, 
			DateTime ExpirationDate)
		{
			carNumber       = Number;
			carModelText    = ModelText;
			parkingPlace    = ParkingPlace;
			visitorFullName = VisitorFullName;
			visitDate       = VisitDate;
			expirationDate  = ExpirationDate;
		}
	}
}
