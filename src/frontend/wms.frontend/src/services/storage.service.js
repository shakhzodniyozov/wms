import { axios } from "../axios/axios";

class StorageService {
    #baseURL = "storage";

    async generateAddresses(data) {
        return await axios.post(`${this.#baseURL}/generate-addresses`, data);
    }

    async getAddresses() {
        return await axios.get(`${this.#baseURL}`);
    }

    async getProductsInCell(addressId) {
        return await axios.get(`${this.#baseURL}/${addressId}`);
    }
}

export default new StorageService();