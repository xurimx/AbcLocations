import axios from '../helpers/api';
const initState = () => ({
    message: 'locations vuex module',
    query: {
      search: '',
      orderBy: '',
      orderDirection: '',
      page: 1,
      limit: 10
    },

    totalItems: 0,
    currentPage: 1,
    filteredItems: 0,
    totalPages: 1,

    selectedLocation: {},
    locations: [],
    showInfoModal: false,
    showCreateModal: false,
    showDeleteModal: false
});

export default {
    namespaced: true,
    state: initState,
    actions: {
        fetchLocations: async ({commit, state}) => {
            let query = state.query;
            let response = await axios.get(
            `locations?page=${query.page !== undefined || null ? query.page : 1}`+
                `${query.search !== undefined || null || '' ? '&search='+query.search : ''}`+
                `${query.limit !== undefined || null || '' ? '&limit='+query.limit : ''}`+
                `${query.orderBy !== undefined || null ? '&orderBy='+query.orderBy : ''}`+
                `${query.orderDirection !== undefined || null ? '&orderDirection='+query.orderDirection : ''}`);

            commit('setLocations', {locations: response.data.items});
            commit('setPagination', {
                totalItems: response.data.totalItems,
                currentPage: response.data.currentPage,
                filteredItems: response.data.filteredItems,
                totalPages: response.data.totalPages,
            });
        },

        deleteLocation: async ({dispatch}, {id}) => {
            await axios.delete('locations/'+id);
            await dispatch('fetchLocations');
        },
        fetchLocation: async ({commit}, {id}) => {
            let response = await axios.get('locations/'+id);
            commit('selectLocation', {location: response.data});
        },
        submitForm: async ({dispatch, commit}, {form}) => {
            await axios.post('locations', {
                name: form.name,
                address: form.address,
                longitude: form.longitude,
                latitude: form.latitude,
                city: form.city
            });
            commit('setLocations', {locations: []});
            await dispatch('fetchLocations');
        },
        updateLocation: async (context, {form}) => {
            await axios.put('locations', {
                id: form.id,
                name: form.name,
                address: form.address,
                longitude: form.longitude,
                latitude: form.latitude,
                city: form.city
            });
        }
    },
    mutations: {
        setLocations: (state, {locations}) => {
            state.locations = locations;
        },
        selectLocation: (state, {location}) => {
            state.selectedLocation = location;
        },
        toggleInfoModal: (state) => {
            state.showInfoModal = !state.showInfoModal;
        },
        toggleCreateModal: (state) => {
            state.showCreateModal = !state.showCreateModal;
        },
        toggleDeleteModal: (state) => {
            state.showDeleteModal = !state.showDeleteModal;
        },
        setPagination: (state, {totalItems, currentPage, filteredItems, totalPages}) => {
            state.totalItems = totalItems;
            state.currentPage = currentPage;
            state.filteredItems = filteredItems;
            state.totalPages = totalPages;
        },
        setQuery: (state, {query}) => {
            state.query = query;
        }
    },
    getters: {
        locations: (state) => {
            return state.locations;
        },
        showInfo: (state) => {
            return state.showInfoModal;
        },
        showCreate: (state) => {
            return state.showCreateModal;
        },
        showDelete: (state) => {
            return state.showDeleteModal;
        },
        selectedLocation: (state) => {
            return state.selectedLocation;
        },
        pagination: (state) => {
            return {
                totalItems: state.totalItems,
                currentPage: state.currentPage,
                filteredItems: state.filteredItems,
                totalPages: state.totalPages,
            }
        },
        query: (state) => {
            return state.query;
        }
    }
}
