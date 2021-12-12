class API{
    constructor() {
        this.baseUrl = document.location.origin + "/api"
    }
    
    async getAllTodos(){
        try{
            const response = await fetch(this.baseUrl+"/todos")
            return await response.json()   
        }catch (e){
            console.error(e)
            return null
        }
    }
    
    async getTodo(id){
        try{
            const response = await fetch(this.baseUrl+"/todos/"+id)
            return await response.json()
        }catch (e){
            console.error(e)
            return null
        }
    }
    
    async modifyTodo(todo){
        try{
            const response = await fetch(this.baseUrl+"/todos/"+todo.id, {
                method: "PUT",
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(todo)
            })
            return await response.json()
        }catch (e){
            console.error(e)
            return null
        }
    }
}