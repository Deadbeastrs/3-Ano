class SelectBox {
    constructor(id) {
        this.getNodes(id);
        this.addEventListeners(id);
    }
    
    getNodes(id) {
        this.title = document.querySelector(".selectbox-title"+id);
        this.links = Array.from(document.querySelectorAll(".selectbox-link"+id));
        this.resultNodes = Array.from(document.querySelectorAll(".selectbox-result"+id));
        this.cardNode = document.querySelector(".accordion-select" +id+ " .card");
    }
    
    addEventListeners(id) {
        this.links.map(link => link.addEventListener("click", this.changeContent.bind(this)));
    }
    
    changeContent(event) {
        event.preventDefault();
        
        const closestLink = event.target.closest("a");
        const closestLinkClone = closestLink.cloneNode(true);
        closestLinkClone.querySelector("span").remove();
        
        const iconNode = this.title.lastChild.cloneNode(true);
        
        this.title.innerHTML = closestLinkClone.textContent;
        this.title.appendChild(iconNode);
        
        this.title.click();
        
        const resultIndex = this.links.indexOf(closestLink);
        this.showResult(resultIndex);
    }
    
    showResult(index) {
        this.resultNodes.map(resultNode => resultNode.classList.remove("selectbox-result-show"));
        this.resultNodes[index].classList.add("selectbox-result-show");
        this.cardNode.classList.add("card-selected");
    }
}

const selectBox = new SelectBox(1);

const selectBox2 = new SelectBox("");