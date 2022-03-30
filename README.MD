# TextMark

TextMark is an open source text annotation tool for humans. It provides annotation features for text classification, and text annotation tasks. So, you can create labeled data for sentiment analysis, named entity recognition, text summarization and so on. Just create a project, upload data and start annotating. You can build a dataset in hours.

## Demo

You can download and try the annotation demo on Docker on your PC. To install it on your Pc, just run the "docker-Compose up" command and it will automatically download all the requirments and their dependencies. 

## Documentation

To read the user manual, first you need to run the project and login to the Admin section by using the below information: 
[Username: Admin] ---- [Password: 12345]. 


### Docker Compose

You need to install Git and to clone the repository:

```bash
git clone https://github.com/aminooei/TextMark
cd TxetMark
```

_Note for Windows developers:_ Be sure to configure git to correctly handle line endings or you may encounter `status code 127` errors while running the services in future steps. Running with the git config options below will ensure your git directory correctly handles line endings.


After running the following command, access <http://localhost:8080/>.



## Contribution

As with any software, TextMark is under continuous development. If you have requests for features, please file an issue describing your request. Also, if you want to see work towards a specific feature, feel free to contribute by working towards it. The standard procedure is to fork the repository, add a feature, fix a bug, then file a pull request that your changes are to be merged into the main repository and included in the next release.



## Citation

```tex
@misc{TextMark,
  title={{TextMark}: Text Annotation Tool for Human},
  url={https://github.com/aminooei/TextMark},
  note={Software available from https://github.com/aminooei/TextMark},
  author={
    Amin Minouei},
  year={2022},
}
```

## Contact

For help and feedback, please feel free to contact [the author](https://github.com/aminooei).
