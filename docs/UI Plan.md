# Resources

User clicks sidebar Resources item and redirected to **Resource List**.

**Resource List** calls `(ResourcesController.GetResources (api/factories/resources/))` and shows list of the resources:
- resource would show how much is being extracted out of the total potention on the map

User clicks on a resource and is redirected to **Resource Details**.

## Resource Details

- Calls `(ResourcesController.GetResourceDetails (api/factories/resources/{resourceId}))` to get details of the resource.
- Calls `(ResourcesController.GetResourceNodes (api/factories/resources/{resourceId}/nodes))` to get a list of the nodes for this resource
	- Want to use leaflet.js to show the node on the game map
	- Maybe organize nodes by Biome and have a little map beside it with the nodes numbered and showing on the map
	- Want to show whether the node has been extracted or not - if extracted it should show the simple extraction details
	      or at least make it obvious which ones are extracted already vs ones that are free
- User clicks an Extract button or Expands the node item to indicate they want Extract the resource
	- Calls `(ResourcesController.GetResourceExtractors (api/factories/resources/{resourceId}/extractors))` to get the extractors that can be used
		  for this resource.
	- Should show how much each extractor can extract
	- User can enter how much to extract and UI will calculate how many shards that amounts to and indicate in the UI? ??do i care about shards??
- User clicks Extract button and calls `(ResourceNodeExtractionsController.ExtractResourceNode (api/factories/resource-node-extractions))`
	- On success, User is redirected back to **Resource Details** which will now show the updated details
