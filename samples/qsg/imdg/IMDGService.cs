using System;

using GigaSpaces.Core;
using GigaSpaces.Core.Admin.ServiceGrid;
using GigaSpaces.Core.Admin.ServiceGrid.Manager;
using GigaSpaces.Core.Admin.ServiceGrid.ProcessingUnit;
using GigaSpaces.Core.Admin.ServiceGrid.Space;
using GigaSpaces.Core.Exceptions;

public class IMDGService {

	String spaceName = "./xapTutorialSpace";

	public void startDataGrid() {
		try {
			// create an admin instance to interact with the cluster
			IServiceGridAdmin admin = new ServiceGridAdminBuilder().CreateAdmin();

			// locate a grid service manager and deploy a partioned data grid
			// with 2 primaries and one backup for each primary
			IGridServiceManager mgr = admin.GridServiceManagers.WaitForAtLeastOne();

			IProcessingUnit pu = mgr.Deploy(new SpaceDeployment(spaceName).Partitioned(2, 1));

			ISpaceProxy spaceProxy = pu.WaitForSpace().SpaceProxy;

		} catch (Exception e) {
			// already deployed, do nothing
			Console.WriteLine(e.StackTrace);
		}
	}

	public void creatVersionedSpace()
	{
		// Embedded Space
		String url = "/./xapTutorialSpace";

		// Create the SpaceProxy
		ISpaceProxy spaceProxy = GigaSpacesFactory.FindSpace(url);
		spaceProxy.OptimisticLocking = true;
	}
}
