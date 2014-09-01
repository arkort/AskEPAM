using System.Collections;
using System.Collections.Generic;
using AskEpamWCFService.Interfaces.Entities;

namespace AskEpamWCFService.Interfaces.DAO
{
	public interface IDAO
	{
	    IEntity Get(IFilter filter);

	    IEnumerable<IEntity> GetList(IFilter filter);
	}
}
