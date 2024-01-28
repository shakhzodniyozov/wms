import { axios } from "../axios/axios";

class SupplyOfGoods {
    #baseURL = "supply";

    async create(data) {
        return await axios.post(`${this.#baseURL}`, data);
    }

    async update(data) {
        return await axios.put(`${this.#baseURL}`, data);
    }

    async getSupplies() {
        return await axios.get(`${this.#baseURL}`);
    }

    async getById(id) {
        return await axios.get(`${this.#baseURL}/${id}`);
    }

    async delete(id) {
        return await axios.delete(`${this.#baseURL}/${id}`);
    }
}

export default new SupplyOfGoods();