import * as React from 'react';
//import ReactDOM from 'react-dom';
import SingleItemAdder from './singleItemAdder.js'

class AddItemToStorage extends React.Component  {
  constructor(props){
    super(props);
    this.onChangeHandleCat = this.onChangeHandleCat.bind(this);
    this.state = {
        token: props.token,
        storageId: props.storageId,
        categories: 0,
        catSelected: 0,
        loadedCategories: null,
        rand: Math.random(),
        user: props.user
      };
    }
 

    
    componentDidMount() {

          const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'accept: text/plain', 'Authorization' : this.state.token}
    };



        fetch("http://localhost:8080/api/Category",requestOptions)
          .then(res => res.json())
          .then(json => this.setState({ categories: json})).then(this.setState({loadedCategories:true}))
        
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
            return <div><SingleItemAdder token={this.state.token} user={this.state.user} storageID={this.state.storageId} key={this.state.rand} categoryId={this.state.catSelected} /></div>          
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

