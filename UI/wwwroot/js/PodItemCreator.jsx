import React from 'react';
import { Calculator } from "./Calculator.jsx";
import { RecipeList } from "./RecipeList.jsx";

export class PodItemCreator extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [],
            selectedItem: null,
            selectedRecipe: null
        };

        this.selectItem = this.selectItem.bind(this);
        this.selectRecipe = this.selectRecipe.bind(this);
    }

    loadItemsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "items", true);
        xhr.onload = () => {
            const items = JSON.parse(xhr.responseText);
            this.setState({ items: items });
        };
        xhr.send();
    }

    selectItem(e) {
        this.setState({
            selectedItem: e.target.value,
            selectedRecipe: null
        });

    }

    selectRecipe(recipe) {
        this.setState({
            selectedRecipe: recipe
        });
    }

    componentDidMount() {
        this.loadItemsFromServer();
    }

    render() {
        return (
            <div className="ui centered grid">
                <div className="eight wide column">
                    <div className="ui center aligned blue very padded text raised segment">
                        <h2 className="ui horizontal divider header">
                            Item
                        </h2>
                        <select className="ui fluid selection dropdown" onChange={(e) => this.selectItem(e)}>
                            {this.state.items.map((item) => <option key={item.code} value={item.code}>{item.name}</option>
                            )}
                        </select>
                        {this.state.selectedItem != null &&
                            <RecipeList item={this.state.selectedItem} selectedRecipe={this.state.selectedRecipe} onSelectRecipe={this.selectRecipe} />}
                    </div>
                </div>
                {this.state.selectedRecipe != null &&
                    <Calculator item={this.state.selectedItem} recipe={this.state.selectedRecipe} />}
            </div>
        );
    }
}
