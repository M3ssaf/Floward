namespace Common.Enum
{
    public enum GeneralStatus
    {
        [EnumRepresentation(0, "Operation Successful")]
        OperationSuccessful = 0,
        
        [EnumRepresentation(1, "Operation Failed")]
        OperationFailed = 1,

        [EnumRepresentation(2, "Application User Not Found")]
        AppUserNotFound = 2,

        [EnumRepresentation(3, "Application User Not Authorized")]
        AppUserNotAuthorized = 3,

        [EnumRepresentation(4, "Incorrect Password")]
        IncorrectPassword = 4,

        [EnumRepresentation(5, "Application User Is Not Active")]
        AppUserIsNotActive = 5,

        [EnumRepresentation(6, "Email Is Not Confirmed")]
        EmailIsNotConfirmed = 6,

        [EnumRepresentation(7, "Application User LockedOut")]
        AppUserLockedOut = 7,

        [EnumRepresentation(8, "Password And Confirmed Password Are Not Matching")]
        PasswordAndConfirmedPasswordAreNotSame = 8,

        [EnumRepresentation(9, "User Role Not Found")]
        UserRoleNotFound = 9,

        [EnumRepresentation(10, "Failed To Create User")]
        FailedToCreateUser = 10,

        [EnumRepresentation(11, "Email Already Exists")]
        EmailAlreadyExists = 11,

        [EnumRepresentation(12, "Failed To Add Product")]
        FailedToAddProduct = 12,

        [EnumRepresentation(13, "Failed To Delete Product")]
        FailedToDeleteProduct = 13,

        [EnumRepresentation(14, "Failed To Update Product")]
        FailedToUpdateProduct = 14,

        [EnumRepresentation(15, "No Products Were Found")]
        NoProductsWereFound = 15
    }
}
