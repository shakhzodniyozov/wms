import { axios } from "../axios/axios";

class ProductService {
    #baseURL = "product";

    async getAll() {
        return await axios.get(this.#baseURL);
    }

    async getProductById(id) {
        return await axios.get(`${this.#baseURL}/${id}`);
    }

    async getPreliminaryDataForNewProduct() {
        return await axios.get(`${this.#baseURL}/new/preliminary`);
    }

    async create(product) {
        return await axios.post(`${this.#baseURL}`, product);
    }

    async delete(productId) {
        return await axios.delete(`${this.#baseURL}/${productId}`);
    }

    async update(product) {
        return await axios.put(`${this.#baseURL}`, product);
    }
}

export default new ProductService();