export type CurrentUser = {
    id: string;
    auth0UserId: string;
    roles: UserRole[];
};

export type UserRole = {
    roleCode: string
}