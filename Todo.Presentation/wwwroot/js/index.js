const apiBaseUrl = "https://localhost:5001/api/"

let todoListVue = new Vue({
    el: '#todo-list',
    data: {
        list: [],
        refreshing: false
    },
    methods: {
        refresh: async function(){
            this.refreshing = true
            await fetch(apiBaseUrl+"todos")
                .then(res => res.json())
                .then(data => {
                    this.list = data
                })
                .finally(() => this.refreshing = false)
        }
    }
})