import { axios } from "../axios/axios";

class ManufactorService {
    #baseRequestURL = "models"

    async getManufacturersWithModels() {
        return await axios.get(`${this.#baseRequestURL}/manufacturers-with-models`);
    }

    async getManufacturers() {
        return await axios.get(`${this.#baseRequestURL}/manufacturers`);
    }
}

export default new ManufactorService();