<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Appleboiy/Chess-ProjectToTheMoon">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Chess game</h3>

  <p align="center">
    Chess game by java
    <br />
    <a href="https://github.com/Appleboiy/Chess-ProjectToTheMoon"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Appleboiy/Chess-ProjectToTheMoon">View Demo</a>
    ·
    <a href="https://github.com/Appleboiy/Chess-ProjectToTheMoon/issues">Report Bug</a>
    ·
    <a href="https://github.com/Appleboiy/Chess-ProjectToTheMoon/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project
Chess simulation. Click on the piece to move, and the location to move it to. If the action is legal, it will be taken. The game is done once a King enters checkmate, or once there is a draw.

[![Product Name Screen Shot][product-screenshot]](https://example.com)

### Pieces
There are six different pieces:
* Pawn
* Bishop
* Rook
* Knight
* Queen
* King

### Pawn
>The pawn may move one square forward, or two squares forward on its first action, given that it does not move over any other pieces. The pawn may capture by moving one square diagonally forward.

### Bishop
> The bishop may move or capture in any amount of squares diagonally, given that it does not move over any other pieces.

### Rook
>The rook may move or capture in any amount of squares horizontally or vertically, given that it does not move over any other pieces.

### Knight
>The knight may move one square vertically and two squares horizontally, or vice-versa. The knight may move over other pieces.

### Queen
>The queen may move or capture in any amount of squares horizontally or vertically or diagonally, given that it does not move over any other pieces.

### King
>The king may move to any square which is adjacent to it, given that it does not move into check.

## Special Actions
>There are three special actions:
* Pawn Promotion
* Castling
* En Passant

### Pawn Promotion
>If a pawn reaches the other end of the board, the user may pick whether to promote it to a queen, rook, knight, or bishop.

### Castling
>If the rook and king have not yet moved, and there are no pieces between them, the king may move two squares towards the rook, and the rook may move to the adjacent square in which the king moved from, given that the king was not in check, will not move into check, and the path between where the king moved from and is moving to do not have any squares which are in check.

### En Passant
>Immediately after a pawn moves two squares, a pawn may capture it as if it had only moved one square.

## Special States
There are three special states:
* Check
* Checkmate
* Draw

### Check
>The king is said to be in check when the next move by the opponent would capture it.

### Checkmate
>A checkmate occurs when the king is in check, and any action taken results in the king still being in check.

### Draw
There are four ways for a draw to occur:
1. Stalemate: occurs when the king is not in check, but any action taken results in the king being in check.
2. 50 move rule: 50 moves from both white and black occur without any pawns moving or any pieces being captured.
3. Threefold repetition: the same board repeats three times in a row, meaning the pieces are on the same squares, and the castling and en passant opportunities are the same.
4. Insufficient mating material: not enough pieces to cause a checkmate, which can occur from a lone king against lone king, or king and knight, or king and bishop, or king and two knights

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* [![Next][Next.js]][Next-url]
* [![React][React.js]][React-url]
* [![Vue][Vue.js]][Vue-url]
* [![Angular][Angular.io]][Angular-url]
* [![Svelte][Svelte.dev]][Svelte-url]
* [![Laravel][Laravel.com]][Laravel-url]
* [![Bootstrap][Bootstrap.com]][Bootstrap-url]
* [![JQuery][JQuery.com]][JQuery-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

* The Java version for this project is Java 19, which can be downloaded [here](https://www.oracle.com/java/technologies/javase/jdk19-archive-downloads.html)


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/Appleboiy/Chess-ProjectToTheMoon.git
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [ ] Feature 1
- [ ] Feature 2
- [ ] Feature 3
    - [ ] Nested Feature

See the [open issues](https://github.com/Appleboiy/Chess-ProjectToTheMoon/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

### If it has any problem please contact: 
>contact.chaipat@gmail.com

>Project Link: [https://github.com/Appleboiy/Chess-ProjectToTheMoon](https://github.com/Appleboiy/Chess-ProjectToTheMoon)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* []()
* []()
* []()

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Appleboiy/Chess-ProjectToTheMoon.svg?style=for-the-badge
[contributors-url]: https://github.com/Appleboiy/Chess-ProjectToTheMoon/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Appleboiy/Chess-ProjectToTheMoon.svg?style=for-the-badge
[forks-url]: https://github.com/Appleboiy/Chess-ProjectToTheMoon/network/members
[stars-shield]: https://img.shields.io/github/stars/Appleboiy/Chess-ProjectToTheMoon.svg?style=for-the-badge
[stars-url]: https://github.com/Appleboiy/Chess-ProjectToTheMoon/stargazers
[issues-shield]: https://img.shields.io/github/issues/Appleboiy/Chess-ProjectToTheMoon.svg?style=for-the-badge
[issues-url]: https://github.com/Appleboiy/Chess-ProjectToTheMoon/issues
[license-shield]: https://img.shields.io/github/license/Appleboiy/Chess-ProjectToTheMoon.svg?style=for-the-badge
[license-url]: https://github.com/Appleboiy/Chess-ProjectToTheMoon/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: images/screenshot.png
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com