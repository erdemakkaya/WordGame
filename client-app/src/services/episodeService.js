import Requests from "./base/baseservice";

class episodeService {
    async getAll() {
      let result = await Requests.get('episode');
      return result;
    }
    async get(id) {
      let result = await Requests.get(`episode/${id}`);
      return result;
    }
  
    async getGrammerValues(id) {
      const result = await Requests.get(`episode/${id}/values`);
      return result;
    }
  
    async createOrEdit(entity) {
      const url = `episode`;
      let result = await Requests.post(url, entity);
      return result;
    }
  
    async delete(id) {
      let result = await Requests.del(`episode/${id}`);
      return result;
    }
    async setGrammerValues(id) {
      const result = await Requests.put(`episode/${id}/values`);
      return result;
    }
  }
  
  export default new episodeService();