const api = new API()

const todoListVue = new Vue({
    el: '#todo-list',
    data: {
        list: [],
        refreshing: false
    },
    methods: {
        async refresh(){
            this.refreshing = true
            let newList = await api.getAllTodos()
            if(newList !== null)
                this.list = newList
            this.refreshing = false;
        },
        async complete(item){
            item.isComplete = !item.isComplete
            await api.modifyTodo(item)
            if(item.isComplete)
                this.pushItemToLast(item)
            else
                this.pushItemToFirst(item)
        },
        addTodo(todo){
            this.list.push(todo)
            this.pushItemToFirst(todo)
        },
        pushItemToLast(item){
            this.list.push(this.list.splice(this.list.indexOf(item), 1)[0])
        },
        pushItemToFirst(item){
            this.list.unshift(this.list.splice(this.list.indexOf(item), 1)[0])
        },
        goToDetail(item){
            window.location = document.location.origin + "/todo.html?id="+item.id
        },
        async remove(item){
            let deleted = await api.deleteTodo(item)
            if(deleted)
                this.list.splice(this.list.indexOf(item), 1)
        }
    },
    mounted: function(){
        this.refresh()
    }
})

const todoAddModal = new Vue({
    el: "#add-todo-modal",
    data: {
        title: null,
        description: null,
        isValid: true
    },
    methods: {
        async addItem(){
            this.validate()
            let addTodo = {
                title: this.title,
                description: this.description
            }
            let newTodo = await api.addTodo(addTodo)
            if(newTodo !== null)
                todoListVue.addTodo(newTodo)
            this.reset()
            this.close()
        },
        validate(){
            this.isValid = !!this.title;
        },
        reset(){
            this.title = null
            this.description = null
            this.isValid = true
        },
        close(){
            this.reset()
            $('#add-todo-modal').modal('hide')
        }
    },
    mounted: function(){
        this.reset()
    }
})