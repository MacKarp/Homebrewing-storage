import * as React from 'react';
//import ReactDOM from 'react-dom';
import Item from './item.js';



class ContentSingleStorage extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        items: [],
        storageId: props.storageId,
        showState: 1,
        user: props.user,
        time: Date.now(),
        token: props.token
      };
    }

      componentDidMount() {

        const requestOptions = {
          method: 'GET',
          headers: { 'Content-Type': 'application/json-patch+json', 'Authorization': this.state.token},
      };


        fetch("http://localhost:8080/api/Expire",requestOptions)
        .then(res => res.json())
       // .then(json => this.setState({ items: json}));
        .then(json => this.setState({ items: json.filter(exp => exp.idStorage === this.state.storageId)}))
        setInterval(() => {
          fetch("http://localhost:8080/api/Expire",requestOptions)
        .then(res => res.json())
       // .then(json => this.setState({ items: json}));
        .then(json => this.setState({ items: json.filter(exp => exp.idStorage === this.state.storageId)}))
        }, 5000)
        
      }
      componentWillUnmount() {
        clearInterval(this.interval);
      }
      itemsToSingleItem = item => {
        const expireId = item.expireId;
        const idItem = item.idItem;
        const expirationDate = item.expirationDate;
        return <Item token={this.state.token} expireId={expireId} key={expireId} idItem={idItem} expirationDate={expirationDate}/>;
      };

      renderSwitch(param) {
        switch(param) {
          case 1:
              return(<div id="showAllItems">
                <table cellSpacing="0"><tbody>
                  <tr><td id="nameProdukt">Nazwa produktu</td><td id="expDateTd">data przydatności</td><td id="delTd"></td></tr>
              {this.state.items.map(this.itemsToSingleItem)}
                </tbody></table>
          </div>)
        case 2:
            return(<div></div>)
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


export default ContentSingleStorage;