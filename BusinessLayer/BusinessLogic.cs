namespace BusinessLayer {

    public static class BusinessLogic {

        //Lazy loading of the BusinessLogicImp class
        private static IBusinessLogic? instance;

        public static IBusinessLogic Instance {
            get {
                instance ??= new BusinessLogicImp();
                return instance;
            }
        }
    }

}
