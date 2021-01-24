import * as React from 'react';
//import ReactDOM from 'react-dom';
import AddExpirationItem from './addExpiration.js';

class SingleItemAdder extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        token: props.token,
        postId: null,
        categoryId: props.categoryId,
        items: [],
        itemSelected: 0,
        loadedItems: null,
        dateSelected: '',
        showState: 1,
        storageID: props.storageID,
        user: props.user,

      };
    }

    componentDidMount() {
      const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'accept: text/plain', 'Authorization' : this.state.token}
    };



        fetch("http://localhost:8080/api/Item",requestOptions)
          .then(res => res.json())
        //niefiltrowane  .then(json => this.setState({ storages: json}))
        .then(json => this.setState({ items: json.filter(item => item.idCategory === parseInt(this.state.categoryId,10))}))// FILTROWANE kategortią 
      }


      safePrinter(){
        if(this.state.items!==0){
            return(<select id="items" value={this.state.value} onChange={this.onChangeHandleItems}>
                <option key={0} value={0}>(wybierz)</option>
           {this.state.items.map(this.itemsToSingle)}
           </select>)
        }
      }

    itemsToSingle = item => {
    const itemName = item.itemName;
    const itemId = item.itemId;
    return <option key={itemId} value={itemId}>{itemName}</option>;
  };
    onChangeHandleItems = (e) =>{
        this.setState({itemSelected: e.target.value})
    }
    onChangeDate = (e) =>{
      var datowa = new Date(e.target.value)
      this.setState({dateSelected: datowa.toLocaleDateString('en-US')})
  }
    
  handleStateSet = (e) =>{
        this.setState({showState: parseInt(e.target.value,10)})
}
    
    switcherCheker(){
        if(parseInt(this.state.itemSelected,10)!==0){
            return <div>Data przydatności do spożycia <input type="date" data-date-format="MMMM DD YYYY" id="expDate" name="expDate" onChange={this.onChangeDate}/></div>          
        } 
    }

    addConfirmed(){
        if(this.state.dateSelected!==''){
        return <div><button className="classicButton" value={2} onClick={this.handleStateSet}>Dodaj</button></div>
        
        }
    }

    renderSwitch(param) {
      switch(param) {
        case 1:
            return(<div id="addStorage">
            {this.safePrinter()}
            {this.switcherCheker()}
            {this.addConfirmed()}
    </div>)
      case 2:
          return(<div><AddExpirationItem token={this.state.token} user={this.state.user} idStorage={this.state.storageID} itemID={this.state.itemSelected} expDate={this.state.dateSelected} /></div>)
      default:
          return(<div>Błąd</div>)
      }
  }


    render(){

        return(
          this.renderSwitch(this.state.showState) 
   
        )
    }

}

export default SingleItemAdder;