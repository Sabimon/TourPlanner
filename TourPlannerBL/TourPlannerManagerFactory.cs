namespace TourPlannerBL {
    public static class TourPlannerManagerFactory {
        private static TourPlannerManager manager;

        public static TourPlannerManager GetFactoryManager() {
            if (manager == null) {
                manager = new TourPlannerManagerImpl();
            }
            return manager;
        }
    }
}
