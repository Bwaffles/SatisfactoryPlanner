import React from 'react';
import { Divider, Dropdown, Form, Grid, Header } from 'semantic-ui-react';
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

    selectItem(event, data) {
        console.debug("PodItemCreator => selectItem");
        console.debug("PodItemCreator => data", data);

        this.setState({
            selectedItem: data.value,
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

    getItems() {
        return this.state.items.map((item) => {
            return {
                key: item.code,
                value: item.code,
                text: item.name
            };
        });

    }

    render() {
        console.debug("PodItemCreator => render()");
        console.debug("PodItemCreator => state", this.state);

        const { selectedItem, selectedRecipe } = this.state;

        return (
            <Form>
                <Grid>
                    <Grid.Row>
                        <Grid.Column>
                            <Divider horizontal>
                                <Header as="h3">New Pod</Header>
                            </Divider>
                        </Grid.Column>
                    </Grid.Row>
                    <Grid.Column width="8">
                        <Form.Field>
                            <label>Number</label>
                            <input type="number" />
                        </Form.Field>
                        <Form.Field>
                            <label>Item</label>
                            <Dropdown
                                placeholder='Select item'
                                fluid
                                search
                                selection
                                options={this.getItems()}
                                onChange={this.selectItem}
                            />
                        </Form.Field>

                        {selectedItem != null &&
                            <RecipeList
                                item={selectedItem}
                                selectedRecipe={selectedRecipe}
                                onSelectRecipe={this.selectRecipe}
                            />
                        }

                    </Grid.Column>
                    <Grid.Column width="8">
                        {selectedRecipe != null &&
                            <Calculator
                                item={selectedItem}
                                recipe={selectedRecipe}
                            />
                        }
                    </Grid.Column>
                </Grid>
            </Form>
        );
    }
}
