namespace BscScanner.Extensions.Convert {
    public static class BscConvert {
        
        private const decimal BnbPerGwei = 0.000000000000000001m;

        public static decimal GweiToBnb(decimal gwei) {
            return gwei * BnbPerGwei;
        }

        public static decimal BnbToGwei(decimal bnb) {
            return bnb / BnbPerGwei;
        }
    }
}