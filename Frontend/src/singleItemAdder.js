import * as React from 'react';
//import ReactDOM from 'react-dom';

class SingleItemAdder extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        token: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibWFpbEBvMi5wbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1haWxAbzIucGwiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTYxMTA4MDk1OX0.GElal5oY0zodHyRh3_-jDkjyiPVAQkPYBw1NScwFlkY',
        postId: null,
        categoryId: props.categoryId,
        items: [],
        itemSelected: 0,
        loadedItems: null
      };
    }

    componentDidMount() {
        fetch("http://localhost:8080/api/Item")
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
    
    switcherCheker(){
        if(parseInt(this.state.itemSelected,10)!==0){
            return <div>wybrano: {this.state.itemSelected} - <input type="date" id="expDate" name="expDate"/></div>          
        } else {
            return <div>Wybierz przedmiot</div>
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

export default SingleItemAdder;