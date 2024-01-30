import { axios } from "../axios/axios";

class ProductService {
    #baseUrl = "product";

    async getProductByEAN(ean) {
        return await axios.get(`${this.#baseUrl}/getById?ean=${ean}`);
    }
}

export default new ProductService();