import * as React from 'react';
//import ReactDOM from 'react-dom';
import SingleItemAdder from './singleItemAdder.js'

class AddItemToStorage extends React.Component  {
  constructor(props){
    super(props);
    this.onChangeHandleCat = this.onChangeHandleCat.bind(this);
    this.state = {
        token: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibWFpbEBvMi5wbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1haWxAbzIucGwiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTYxMTA4MDk1OX0.GElal5oY0zodHyRh3_-jDkjyiPVAQkPYBw1NScwFlkY',
        storageId: props.storageId,
        categories: 0,
        catSelected: 0,
        loadedCategories: null,
        rand: Math.random()
      };
    }
 
    componentDidMount() {
        fetch("http://localhost:8080/api/Category")
          .then(res => res.json())
          .then(json => this.setState({ categories: json})).then(this.setState({loadedCategories:true}))
          //niefiltrowane
        // FILTROWANE USEREM .then(json => this.setState({ storages: json.filter(user => user.idUser === this.state.user)}))
        
      }

      safePrinter(){
        if(this.state.categories!==0){
            return(<select id="categories" value={this.state.value} onChange={this.onChangeHandleCat}>
                <option key={0} value={0}>(wybierz)</option>
           {this.state.categories.map(this.categoriesToSingle)}
           </select>)
        }
      }

    categoriesToSingle = category => {
    const categoryName = category.categoryName;
    const catId = category.categoryId;
    return <option key={catId} value={catId}>{categoryName}</option>;
  };
    onChangeHandleCat = (e) =>{
        this.setState({catSelected: e.target.value,rand: Math.random()})
    }

    switcherCheker(){
        if(parseInt(this.state.catSelected,10)!==0){
            return <div><SingleItemAdder key={this.state.rand} categoryId={this.state.catSelected} /></div>          
        } else {
            return <div>Wybierz kategorię</div>
        }
    }

    render(){
        return(
            <div id="addStorage">
                    {this.safePrinter()}
                    {this.switcherCheker()}
            </div>
        )
    }
}




export default AddItemToStorage;

