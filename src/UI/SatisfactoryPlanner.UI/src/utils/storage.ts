const storagePrefix = "satisfactory_planner_";
const worldIdKey = `${storagePrefix}world_id`;

// TODO need to come up with something better to get the world id for the session--thinking Context, but don't want to get too caught up in yet
const storage = {
    getWorldId: (): string => {
        return window.localStorage.getItem(worldIdKey) as string;
    },
    setWorldId: (worldId: string) => {
        window.localStorage.setItem(worldIdKey, worldId);
    },
    clearWorldId: () => {
        window.localStorage.removeItem(worldIdKey);
    },
};

export default storage;
