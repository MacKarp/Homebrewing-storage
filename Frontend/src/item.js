import * as React from 'react';
//import ReactDOM from 'react-dom';
import DelExpire from './delExpire.js';
import ItemName from './itemName.js';



class Item extends React.Component  {
    constructor(props){
      super(props);
      this.state = {
        expireId : props.expireId,
        idItem : props.idItem,
        expirationDate : props.expirationDate,
        showState: 1,
        idCat:props.idCat,
        lel:null,
        token: props.token
        }
      }


      handleStateSet = (e) =>{
        if(e.target.value==='2') { if(window.confirm("Czy na pewno usunąć?")){this.setState({showState: parseInt(e.target.value,10)})}; }
        else {this.setState({showState: parseInt(e.target.value,10)})}
    }




      renderSwitch(param) {
        switch(param) {
          case 1:
              var dataTemp = new Date(this.state.expirationDate);
              var dataAktualna = new Date();
              var colorStyle = "blackDate";
              var dataZaDwaTygodnie = new Date();
              dataZaDwaTygodnie.setDate(dataZaDwaTygodnie.getDate() + 14);
              if(dataTemp<dataAktualna) { colorStyle="redDate"}
              else if(dataTemp<dataZaDwaTygodnie) { colorStyle="semiRedDate"}

              return(
        <tr><td className="itemName"><ItemName token={this.state.token} idItem={this.state.idItem}/></td><td className={colorStyle}> {dataTemp.toLocaleDateString('pl-PL')}</td><td><button className="del_exp" value={2} onClick={this.handleStateSet}></button></td></tr>
          )
        case 2:
            return(<tr><td colSpan="3"><DelExpire token={this.state.token} expireId={this.state.expireId}/></td></tr>)
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
      export default Item;