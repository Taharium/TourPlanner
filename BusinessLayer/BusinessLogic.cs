namespace BusinessLayer {

    public static class BusinessLogic {

        private static IBusinessLogic? instance;

        public static IBusinessLogic Instance {
            get {
                instance ??= new BusinessLogicImp();
                return instance;
            }
        }
    }

}
